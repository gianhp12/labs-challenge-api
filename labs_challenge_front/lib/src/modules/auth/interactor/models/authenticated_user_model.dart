class AuthenticatedUserModel {
  final String name;
  final String email;
  final String token;
  final int expiresIn;

  AuthenticatedUserModel({
    required this.name,
    required this.email,
    required this.token,
    required this.expiresIn,
  });
}