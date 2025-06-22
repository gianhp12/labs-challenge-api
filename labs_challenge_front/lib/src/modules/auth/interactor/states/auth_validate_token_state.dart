import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

sealed class AuthValidateTokenState extends AppState {
  final bool loading;
  final String exception;

  AuthValidateTokenState({
    required this.loading,
    required this.exception,
  });

  factory AuthValidateTokenState.start() => StartAuthValidateTokenState();

  AuthValidateTokenState setLoading() => LoadingAuthValidateTokenState(loading: true);

  AuthValidateTokenState setError(String error) => ErrorAuthValidateTokenState(exception: error);

  AuthValidateTokenState setSuccess() => SuccessAuthValidateTokenState();
}

class StartAuthValidateTokenState extends AuthValidateTokenState {
  StartAuthValidateTokenState({super.exception = '', super.loading = false});
}

class LoadingAuthValidateTokenState extends AuthValidateTokenState {
  LoadingAuthValidateTokenState({required super.loading, super.exception = ''});
}

class ErrorAuthValidateTokenState extends AuthValidateTokenState {
  ErrorAuthValidateTokenState({required super.exception, super.loading = false});
}

class SuccessAuthValidateTokenState extends AuthValidateTokenState {
  SuccessAuthValidateTokenState({super.exception = '', super.loading = false});
}
