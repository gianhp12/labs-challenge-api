import 'dart:convert';
import 'package:labs_challenge_front/src/modules/auth/data/mappers/autenticated_user_mapper.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/shared/errors/app_error.dart';
import 'package:labs_challenge_front/src/shared/errors/common_errors.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_error.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_service.dart';
import 'package:labs_challenge_front/src/shared/utils/api_routes.dart';
import 'package:result_dart/result_dart.dart';

class AuthRepositoryImpl implements AuthRepository {
  final HttpService _httpService;

  AuthRepositoryImpl(this._httpService);

  @override
  AsyncResult<AuthenticatedUserModel, AppError> login(
    String email,
    String password,
  ) async {
    try {
      final result = await _httpService.post(
        ApiRoutes.login,
        body: {"email": email, "password": password},
      );
      final Map<String, dynamic> jsonResult = jsonDecode(result.data);
      final authenticatedUser = AutenticatedUserMapper.fromMap(jsonResult);
      return Success(authenticatedUser);
    } on RemoteRequestError catch (ex) {
      if (ex is BadRequest || ex is NotFound) {
        return Failure(
          ConnectionError(
            errorMessage: ex.error!,
            errorModule: 'auth',
            errorMethod: 'login',
          ),
        );
      }
      return Failure(
        ConnectionError(
          errorMessage: 'Ocorreu um erro interno na API',
          errorModule: 'auth',
          errorMethod: 'login',
        ),
      );
    }
  }

  @override
  AsyncResult<void, AppError> register(
    String username,
    String email,
    String password,
  ) async {
    try {
      await _httpService.post(
        ApiRoutes.registerUser,
        body: {"username": username, "email": email, "password": password},
      );
      return Success(1);
    } on RemoteRequestError catch (ex) {
      if (ex is BadRequest) {
        return Failure(
          ConnectionError(
            errorMessage: ex.error!,
            errorModule: 'auth',
            errorMethod: 'register',
          ),
        );
      }
      return Failure(
        ConnectionError(
          errorMessage: "Ocorreu um erro interno na API",
          errorModule: 'auth',
          errorMethod: 'register',
        ),
      );
    }
  }
}
