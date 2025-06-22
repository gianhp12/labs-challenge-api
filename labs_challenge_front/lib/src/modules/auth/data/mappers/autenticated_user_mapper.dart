import 'package:labs_challenge_front/src/modules/auth/interactor/models/authenticated_user_model.dart';

class AutenticatedUserMapper {
  static AuthenticatedUserModel fromMap(map) {
    return AuthenticatedUserModel(
      name: map["name"],
      email: map["email"],
      token: map["token"],
      expiresIn: map["expiresIn"],
      isEmailConfirmed: map["isEmailConfirmed"]
    );
  }
}
