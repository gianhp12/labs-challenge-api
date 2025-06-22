import 'package:flutter/material.dart';

class PasswordStrengthIndicator extends StatelessWidget {
  final String strengthText;

  const PasswordStrengthIndicator({super.key, required this.strengthText});

  Color get color {
    switch (strengthText) {
      case 'Senha forte':
        return Colors.green;
      case 'Senha média':
        return Colors.orange;
      case 'Senha fraca':
        return Colors.red;
      default:
        return Colors.transparent;
    }
  }
  bool get isVisible => strengthText.isNotEmpty;

  @override
  Widget build(BuildContext context) {
    if (!isVisible) return const SizedBox.shrink();

    return Align(
      alignment: Alignment.centerLeft,
      child: Row(
        children: [
          Container(
            width: 8,
            height: 8,
            decoration: BoxDecoration(
              color: color,
              shape: BoxShape.circle,
            ),
          ),
          const SizedBox(width: 8),
          Text(
            strengthText,
            style: TextStyle(
              fontSize: 13,
              color: color,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ),
    );
  }
}
