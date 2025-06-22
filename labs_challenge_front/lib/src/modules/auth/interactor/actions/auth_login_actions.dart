import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_login_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

class AuthLoginActions extends StateNotifier<AuthLoginState> {
  final AuthRepository _repository;

  AuthLoginActions(this._repository) : super(AuthLoginState.start());

  Future<void> login(String email, String password) async {
    notifySetState((state) => state.setLoading());
    final result = await _repository.login(email, password);
    result.fold(
      (success) {
        final authenticatedUser = success;
        notifySetState(
          (state) => state.setAuthenticatedUser(authenticatedUser),
        );
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
