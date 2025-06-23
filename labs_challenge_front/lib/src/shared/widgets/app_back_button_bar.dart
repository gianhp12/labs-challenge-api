import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';

class AppBackButtonBar extends StatelessWidget implements PreferredSizeWidget {
  final String? title;
  final Color backgroundColor;
  final Color iconColor;
  final VoidCallback? onBack;

  const AppBackButtonBar({
    super.key,
    this.title,
    this.backgroundColor = Colors.transparent,
    this.iconColor = Colors.blue,
    this.onBack,
  });

  @override
  Widget build(BuildContext context) {
    return AppBar(
      backgroundColor: backgroundColor,
      elevation: 0,
      leading: IconButton(
        icon: Icon(Icons.arrow_back, color: iconColor),
        onPressed: onBack ?? () => Modular.to.pop(),
      ),
      title:
          title != null
              ? Text(title!, style: TextStyle(color: iconColor))
              : null,
      centerTitle: true,
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}
