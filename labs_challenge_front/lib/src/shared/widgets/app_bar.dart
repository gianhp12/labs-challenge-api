import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';

class AppAppBar extends StatelessWidget implements PreferredSizeWidget {
  final String title;
  final bool showLogout;
  final List<Widget>? actions;

  const AppAppBar({
    super.key,
    required this.title,
    this.showLogout = true,
    this.actions,
  });

  @override
  Widget build(BuildContext context) {
    return AppBar(
      backgroundColor: Colors.blue.shade50,
      elevation: 0,
      iconTheme: const IconThemeData(color: Colors.black87),
      title: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          const Icon(
            Icons.local_shipping_outlined,
            color: Colors.blue,
            size: 28,
          ),
          const SizedBox(width: 8),
          Text(
            title,
            style: const TextStyle(
              color: Colors.black87,
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      ),
      centerTitle: true,
      actions: [
        ...?actions,
        if (showLogout)
          IconButton(
            tooltip: 'Sair',
            icon: const Icon(Icons.logout, color: Colors.red, size: 25),
            onPressed: () {
              Modular.to.navigate('/');
            },
          ),
      ],
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}
