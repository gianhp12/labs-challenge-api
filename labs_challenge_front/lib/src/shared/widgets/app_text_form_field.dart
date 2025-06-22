import 'package:flutter/material.dart';

class AppTextFormField extends StatelessWidget {
  final TextEditingController controller;
  final FocusNode? focusNode;
  final String label;
  final IconData? icon;
  final String? Function(String?)? validator;
  final TextInputType keyboardType;
  final bool obscureText;
  final void Function(String)? onChanged;

  const AppTextFormField({
    super.key,
    required this.controller,
    this.focusNode,
    required this.label,
    this.icon,
    this.validator,
    this.keyboardType = TextInputType.text,
    this.obscureText = false,
    this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      focusNode: focusNode,
      controller: controller,
      keyboardType: keyboardType,
      obscureText: obscureText,
      decoration: InputDecoration(
        labelText: label,
        prefixIcon: icon != null ? Icon(icon) : null,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(12),
        ),
      ),
      validator: validator,
      onChanged: onChanged,
    );
  }
}
