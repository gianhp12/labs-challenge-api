import 'package:labs_challenge_front/src/shared/errors/app_error.dart';

class ConnectionError extends AppError {
  ConnectionError({
    super.saveInLog = true,
    required super.errorModule,
    required super.errorMessage,
    required super.errorMethod,
  });
}

class RepositoryError extends AppError {
  RepositoryError({
    super.saveInLog = true,
    required super.errorModule,
    required super.errorMessage,
    required super.errorMethod,
  });
}
