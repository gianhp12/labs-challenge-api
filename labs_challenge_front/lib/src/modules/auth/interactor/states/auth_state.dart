import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

sealed class AuthState extends AppState {
  final bool loading;
  final String exception;
  final AuthenticatedUserModel? authenticatedUser;

  AuthState({
    required this.loading,
    required this.exception,
    this.authenticatedUser,
  });

  factory AuthState.start() => StartAuthState();

  AuthState setLoading() => LoadingAuthState(loading: true);

  AuthState setError(String error) => ErrorAuthState(exception: error);
  
  AuthState setAuthenticatedUser(AuthenticatedUserModel authenticatedUser) => GettedAuthState(authenticatedUser: authenticatedUser);
}

class StartAuthState extends AuthState {
  StartAuthState({super.exception = '', super.loading = false});
}

class LoadingAuthState extends AuthState {
  LoadingAuthState({required super.loading, super.exception = ''});
}

class ErrorAuthState extends AuthState {
  ErrorAuthState({required super.exception, super.loading = false});
}

class GettedAuthState extends AuthState {
  GettedAuthState({
    required super.authenticatedUser,
    super.exception = '',
    super.loading = false,
  });
}
