import 'package:labs_challenge_front/src/shared/models/logged_user.dart';

class AutenticatedUserMapper {
  static LoggedUser fromMap(map) {
    return LoggedUser(
      name: map["username"],
      email: map["email"],
      token: map["accessToken"],
      expiresIn: map["expiresIn"],
      isEmailConfirmed: map["isEmailConfirmed"]
    );
  }
}
