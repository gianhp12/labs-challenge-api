class LoggedUser {
  final String token;
  final String name;
  final String email;
  final int expiresIn;
  final bool isEmailConfirmed;

  LoggedUser({
    required this.token,
    required this.name,
    required this.email,
    required this.expiresIn,
    required this.isEmailConfirmed,
  });

  Map<String, dynamic> toMap() {
    return {
      'token': token,
      'name': name,
      'email': email,
      'expiresIn': expiresIn,
      'isEmailConfirmed': isEmailConfirmed,
    };
  }

  factory LoggedUser.fromMap(Map<String, dynamic> map) {
    return LoggedUser(
      token: map['token'] ?? '',
      name: map['name'] ?? '',
      email: map['email'] ?? '',
      expiresIn: map['expiresIn'] ?? 0,
      isEmailConfirmed: map['isEmailConfirmed'] ?? false,
    );
  }
}
