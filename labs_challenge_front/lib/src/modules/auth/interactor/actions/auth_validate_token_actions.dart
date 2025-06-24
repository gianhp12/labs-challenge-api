import 'dart:convert';

import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_validate_token_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthValidateTokenActions extends StateNotifier<AuthValidateTokenState> {
  final AuthRepository _repository;

  AuthValidateTokenActions(this._repository)
    : super(AuthValidateTokenState.start());

  Future<void> resendToken(String email) async {
    notifySetState((state) => state.setLoading());
    var result = await _repository.resendToken(email);
    result.fold(
      (success) {
        notifySetState((state) => state.setSuccessResendToken());
      },
      (failure) {
        notifySetState((state) => state.setError(failure.errorMessage));
      },
    );
  }

  Future<void> validateToken(
    String email,
    String password,
    String token,
  ) async {
    final session = Modular.get<SessionNotifier>();
    notifySetState((state) => state.setLoading());
    var result = await _repository.validateToken(email, token);
    result.fold(
      (success) async {
        final loginResult = await _repository.login(email, password);
        loginResult.fold(
          (success) async {
            final loggedUser = success;
            session.logIn(loggedUser);
            final prefs = await SharedPreferences.getInstance();
            prefs.setString('logged_user', jsonEncode(loggedUser.toMap()));
            notifySetState((state) => state.setSuccessValidateToken());
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
