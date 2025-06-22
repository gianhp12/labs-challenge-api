import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

sealed class AuthLoginState extends AppState {
  final bool loading;
  final String exception;
  final AuthenticatedUserModel? authenticatedUser;

  AuthLoginState({
    required this.loading,
    required this.exception,
    this.authenticatedUser,
  });

  factory AuthLoginState.start() => StartAuthLoginState();

  AuthLoginState setLoading() => LoadingAuthLoginState(loading: true);

  AuthLoginState setError(String error) => ErrorAuthLoginState(exception: error);
  
  AuthLoginState setAuthenticatedUser(AuthenticatedUserModel authenticatedUser) => GettedAuthLoginState(authenticatedUser: authenticatedUser);
}

class StartAuthLoginState extends AuthLoginState {
  StartAuthLoginState({super.exception = '', super.loading = false});
}

class LoadingAuthLoginState extends AuthLoginState {
  LoadingAuthLoginState({required super.loading, super.exception = ''});
}

class ErrorAuthLoginState extends AuthLoginState {
  ErrorAuthLoginState({required super.exception, super.loading = false});
}

class GettedAuthLoginState extends AuthLoginState {
  GettedAuthLoginState({
    required super.authenticatedUser,
    super.exception = '',
    super.loading = false,
  });
}
