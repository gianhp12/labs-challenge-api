import 'package:labs_challenge_front/flavor_config.dart';

class ApiRoutes {
  static String _getBaseUrl() {
    switch (flavor) {
      case Flavor.dev:
        return 'http://localhost:8081';
      case Flavor.stage:
        return 'http://labs-challenge-api:8081';
      case Flavor.prod:
        return 'http://labs-challenge-api:8081';
    }
  }

  //Auth Routes
  static String get registerUser => "${_getBaseUrl()}/api/Auth/register";
  static String get login => "${_getBaseUrl()}/api/Auth/auth";
  static String get validateEmailToken =>
      "${_getBaseUrl()}/api/Auth/validateEmailToken";
}
