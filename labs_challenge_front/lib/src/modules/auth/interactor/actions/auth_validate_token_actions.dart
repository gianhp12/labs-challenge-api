import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_validate_token_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';

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
    final result = await _repository.validateToken(email, token);
    if (result.isSuccess()) {
      final loginResult = await _repository.login(email, password);
      if (loginResult.isSuccess()) {
        final loggedUser = loginResult.getOrNull()!;
        session.logIn(loggedUser);
        notifySetState((state) => state.setSuccessValidateToken());
      } else {
        final error = loginResult.exceptionOrNull()!;
        notifySetState((state) => state.setError(error.errorMessage));
      }
    } else {
      final error = result.exceptionOrNull()!;
      notifySetState((state) => state.setError(error.errorMessage));
    }
  }
}
