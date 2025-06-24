import 'package:flutter/material.dart';

class AppSnackBar extends StatelessWidget {
  final String message;
  final bool isSuccess;

  const AppSnackBar({
    super.key,
    required this.message,
    required this.isSuccess,
  });

  Color get backgroundColor =>
      isSuccess ? const Color(0xFFD4EDDA) : const Color(0xFFFFF3CD);

  Color get iconColor =>
      isSuccess ? const Color(0xFF155724) : const Color(0xFF856404);

  IconData get icon =>
      isSuccess ? Icons.check_circle_rounded : Icons.warning_amber_rounded;

  @override
  Widget build(BuildContext context) {
    return Container(
      color: backgroundColor,
      padding: const EdgeInsets.all(10),
      child: Row(
        children: [
          Icon(icon, color: iconColor),
          const SizedBox(width: 12),
          Expanded(
            child: Text(
              message,
              style: TextStyle(color: iconColor, fontWeight: FontWeight.w600),
            ),
          ),
        ],
      ),
    );
  }
}
