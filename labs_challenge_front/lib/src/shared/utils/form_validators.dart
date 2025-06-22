class FormValidators {
  static String? isEmail(String? value) {
    final requiredError = isRequired(value, fieldName: 'E-mail');
    if (requiredError != null) return requiredError;
    if (!RegExp(r'^[^@]+@[^@]+\.[^@]+').hasMatch(value!)) {
      return 'Digite um e-mail válido';
    }
    return null;
  }

  static String? isRequired(String? value, {String fieldName = 'Campo'}) {
    if (value == null || value.isEmpty) {
      return '$fieldName é obrigatório';
    }
    return null;
  }

  static String? minLength(
    String? value,
    int min, {
    String fieldName = 'Campo',
  }) {
    final requiredError = isRequired(value, fieldName: fieldName);
    if (requiredError != null) return requiredError;

    if (value!.length < min) {
      return '$fieldName deve ter pelo menos $min caracteres';
    }
    return null;
  }

  static String? isCpf(String? value) {
    final requiredError = isRequired(value, fieldName: 'CPF');
    if (requiredError != null) return requiredError;

    if (!RegExp(r'^\d{11}$').hasMatch(value!)) {
      return 'CPF inválido';
    }
    return null;
  }

  static String? passwordStrengthValidator(String? value, {int minLength = 8}) {
    if (value == null || value.isEmpty) {
      return 'Senha é obrigatória';
    }
    if (value.length < minLength) {
      return 'Senha deve ter pelo menos $minLength caracteres';
    }
    final hasUpper = RegExp(r'[A-Z]').hasMatch(value);
    final hasDigit = RegExp(r'\d').hasMatch(value);
    final missing = <String>[];
    if (!hasUpper) missing.add('uma letra maiúscula');
    if (!hasDigit) missing.add('um número');
    if (missing.isNotEmpty) {
      return 'Senha média: inclua ${missing.join(' e ')}';
    }
    return null;
  }

  static String passwordStrengthLevel(String value) {
    final hasLower = RegExp(r'[a-z]').hasMatch(value);
    final hasUpper = RegExp(r'[A-Z]').hasMatch(value);
    final hasDigit = RegExp(r'\d').hasMatch(value);
    final conditionsMet = [hasLower, hasUpper, hasDigit].where((c) => c).length;
    if (conditionsMet <= 1) return 'Senha fraca';
    if (conditionsMet == 2) return 'Senha média';
    if (conditionsMet == 3) return 'Senha forte';
    return '';
  }
}
