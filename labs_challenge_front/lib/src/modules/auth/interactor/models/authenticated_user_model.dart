class AuthenticatedUserModel {
  final String name;
  final String email;
  final String token;
  final int expiresIn;
  final bool isEmailConfirmed;

  AuthenticatedUserModel({
    required this.name,
    required this.email,
    required this.token,
    required this.expiresIn,
    required this.isEmailConfirmed,
  });
}