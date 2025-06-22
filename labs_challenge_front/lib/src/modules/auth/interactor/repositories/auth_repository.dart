import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';
import 'package:labs_challenge_front/src/shared/errors/app_error.dart';
import 'package:result_dart/result_dart.dart';

abstract interface class AuthRepository {
  AsyncResult<AuthenticatedUserModel, AppError> login(
    String email,
    String password,
  );

  AsyncResult<void, AppError> register(
    String username,
    String email,
    String password,
  );
}
