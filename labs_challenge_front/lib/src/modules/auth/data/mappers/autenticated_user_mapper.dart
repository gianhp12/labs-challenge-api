import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';

class AutenticatedUserMapper {
  static AuthenticatedUserModel fromMap(map) {
    return AuthenticatedUserModel(
      name: map["username"],
      email: map["email"],
      token: map["accessToken"],
      expiresIn: map["expiresIn"],
      isEmailConfirmed: map["isEmailConfirmed"]
    );
  }
}
