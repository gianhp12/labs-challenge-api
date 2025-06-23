import 'dart:convert';

import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_register_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthRegisterActions extends StateNotifier<AuthRegisterState> {
  final AuthRepository _repository;

  AuthRegisterActions(this._repository) : super(AuthRegisterState.start());

  Future<void> register(String username, String email, String password) async {
    final session = Modular.get<SessionNotifier>();
    notifySetState((state) => state.setLoading());
    final registerResult = await _repository.register(
      username,
      email,
      password,
    );
    registerResult.fold(
      (success) async {
        final loginResult = await _repository.login(email, password);
        loginResult.fold(
          (success) async {
            final loggedUser = success;
            session.logIn(loggedUser);
            final prefs = await SharedPreferences.getInstance();
            prefs.setString('logged_user', jsonEncode(loggedUser.toMap()));
            notifySetState((state) => state.setSuccess(loggedUser));
          },
          (failure) {
            notifySetState((state) => state.setError(failure.errorMessage));
          },
        );
      },
      (failure) {
        notifySetState((state) => state.setError(failure.errorMessage));
      },
    );
  }
}
