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

  static String? minLength(String? value, int min, {String fieldName = 'Campo'}) {
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
}
