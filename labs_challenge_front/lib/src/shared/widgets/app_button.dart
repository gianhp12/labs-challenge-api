import 'package:flutter/material.dart';

enum AppButtonType { elevated, outlined }

class AppButton extends StatelessWidget {
  final VoidCallback? onPressed;
  final String label;
  final bool isLoading;
  final AppButtonType type;
  final Color backgroundColor;
  final Color borderColor;
  final Color textColor;
  final double fontSize;
  final double borderRadius;
  final EdgeInsetsGeometry padding;
  final double? width;
  final double? height;

  const AppButton({
    super.key,
    required this.onPressed,
    required this.label,
    this.isLoading = false,
    this.type = AppButtonType.elevated,
    this.backgroundColor = Colors.blue,
    this.borderColor = Colors.blue,
    this.textColor = Colors.white,
    this.fontSize = 16,
    this.borderRadius = 12,
    this.padding = const EdgeInsets.symmetric(vertical: 16),
    this.width,
    this.height,
  });

  @override
  Widget build(BuildContext context) {
    final child = isLoading
        ? SizedBox(
            width: 24,
            height: 24,
            child: CircularProgressIndicator(
              color: type == AppButtonType.outlined ? borderColor : textColor,
              strokeWidth: 2.5,
            ),
          )
        : Text(
            label,
            style: TextStyle(
              fontSize: fontSize,
              color: textColor,
              fontWeight: FontWeight.w600,
            ),
          );
    final ButtonStyle style;
    if (type == AppButtonType.outlined) {
      style = OutlinedButton.styleFrom(
        padding: padding,
        side: BorderSide(color: borderColor),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(borderRadius),
        ),
        foregroundColor: textColor,
      );
    } else {
      style = ElevatedButton.styleFrom(
        backgroundColor: backgroundColor,
        padding: padding,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(borderRadius),
        ),
      );
    }
    final button = type == AppButtonType.outlined
        ? OutlinedButton(
            onPressed: isLoading ? null : onPressed,
            style: style,
            child: child,
          )
        : ElevatedButton(
            onPressed: isLoading ? null : onPressed,
            style: style,
            child: child,
          );
    return SizedBox(
      width: width,
      height: height,
      child: button,
    );
  }
}
