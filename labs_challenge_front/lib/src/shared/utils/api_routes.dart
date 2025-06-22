import 'package:labs_challenge_front/flavor_config.dart';

class ApiRoutes {
  static String _getBaseUrl() {
    switch (flavor) {
      case Flavor.dev:
        return 'http://127.0.0.1:8081';
      case Flavor.stage:
        return 'http://labs-challenge-api:8081';
      case Flavor.prod:
        return 'http://labs-challenge-api:8081';
    }
  }

  //Auth Routes
  static String get registerUser => "${_getBaseUrl()}/api/v1/Auth/register";
  static String get login => "${_getBaseUrl()}/api/v1/Auth/login";
  static String get validateEmailToken =>
      "${_getBaseUrl()}/api/v1/Auth/validate-email-token";
}
