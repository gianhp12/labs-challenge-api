import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';

class BackToHomeButton extends StatelessWidget {
  final VoidCallback? onPressed;

  const BackToHomeButton({super.key, this.onPressed});

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.center,
      child: TextButton.icon(
        onPressed: onPressed ?? () => Modular.to.navigate('/'),
        icon: const Icon(Icons.home_outlined, color: Colors.blue),
        label: const Text(
          'Voltar ao Início',
          style: TextStyle(color: Colors.blue),
        ),
        style: TextButton.styleFrom(
          padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
          textStyle: const TextStyle(fontSize: 14, fontWeight: FontWeight.w600),
        ),
      ),
    );
  }
}
