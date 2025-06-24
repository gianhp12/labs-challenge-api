import 'dart:convert';

import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_login_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthLoginActions extends StateNotifier<AuthLoginState> {
  final AuthRepository _repository;

  AuthLoginActions(this._repository) : super(AuthLoginState.start());

  Future<void> login(String email, String password) async {
    final session = Modular.get<SessionNotifier>();
    notifySetState((state) => state.setLoading());
    final result = await _repository.login(email, password);
    result.fold(
      (success) async {
        final loggedUser = success;
        if (loggedUser.isEmailConfirmed) {
          session.logIn(loggedUser);
          final prefs = await SharedPreferences.getInstance();
          prefs.setString('logged_user', jsonEncode(loggedUser.toMap()));
        }
        notifySetState((state) => state.setLoggedUser(loggedUser));
      },
      (failure) {
        notifySetState((state) => state.setError(failure.errorMessage));
      },
    );
  }

  void reset() {
    notifySetState((state) => AuthLoginState.start());
  }
}
