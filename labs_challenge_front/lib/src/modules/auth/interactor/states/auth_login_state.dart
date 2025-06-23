import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';

sealed class AuthLoginState extends AppState {
  final bool loading;
  final String exception;
  final LoggedUser? loggedUser;

  AuthLoginState({
    required this.loading,
    required this.exception,
    this.loggedUser,
  });

  factory AuthLoginState.start() => StartAuthLoginState();

  AuthLoginState setLoading() => LoadingAuthLoginState(loading: true);

  AuthLoginState setError(String error) => ErrorAuthLoginState(exception: error);
  
  AuthLoginState setLoggedUser(LoggedUser loggedUser) => GettedAuthLoginState(loggedUser: loggedUser);
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
    required super.loggedUser,
    super.exception = '',
    super.loading = false,
  });
}
