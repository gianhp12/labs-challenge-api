class LoggedUser {
  final int id;
  final String token;
  final String name;
  final String email;

  LoggedUser({
    required this.id,
    required this.token,
    required this.name,
    required this.email,
  });
}