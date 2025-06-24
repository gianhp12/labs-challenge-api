import 'package:labs_challenge_front/src/shared/errors/app_error.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:result_dart/result_dart.dart';

abstract interface class AuthRepository {
  AsyncResult<LoggedUser, AppError> login(String email, String password);

  AsyncResult<void, AppError> register(
    String username,
    String email,
    String password,
  );

  AsyncResult<void, AppError> resendToken(String email);

  AsyncResult<void, AppError> validateToken(String email, String token);
}
