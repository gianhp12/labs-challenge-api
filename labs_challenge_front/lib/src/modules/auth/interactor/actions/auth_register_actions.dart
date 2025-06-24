import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_register_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

class AuthRegisterActions extends StateNotifier<AuthRegisterState> {
  final AuthRepository _repository;

  AuthRegisterActions(this._repository) : super(AuthRegisterState.start());

  Future<void> register(String username, String email, String password) async {
    notifySetState((state) => state.setLoading());
    final result = await _repository.register(username, email, password);
    result.fold(
      (success) {
        notifySetState((state) => state.setSuccess());
      },
      (failure) {
        notifySetState((state) => state.setError(failure.errorMessage));
      },
    );
  }
}
