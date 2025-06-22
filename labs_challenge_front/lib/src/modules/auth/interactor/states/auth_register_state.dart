import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

sealed class AuthRegisterState extends AppState {
  final bool loading;
  final String exception;

  AuthRegisterState({
    required this.loading,
    required this.exception,
  });

  factory AuthRegisterState.start() => StartAuthRegisterState();

  AuthRegisterState setLoading() => LoadingAuthRegisterState(loading: true);

  AuthRegisterState setError(String error) => ErrorAuthRegisterState(exception: error);

  AuthRegisterState setSuccess() => SuccessAuthRegisterState();
}

class StartAuthRegisterState extends AuthRegisterState {
  StartAuthRegisterState({super.exception = '', super.loading = false});
}

class LoadingAuthRegisterState extends AuthRegisterState {
  LoadingAuthRegisterState({required super.loading, super.exception = ''});
}

class ErrorAuthRegisterState extends AuthRegisterState {
  ErrorAuthRegisterState({required super.exception, super.loading = false});
}

class SuccessAuthRegisterState extends AuthRegisterState {
  SuccessAuthRegisterState({super.exception = '', super.loading = false});
}
