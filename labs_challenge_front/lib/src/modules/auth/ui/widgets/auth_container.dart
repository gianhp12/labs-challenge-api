import 'package:flutter/material.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/back_to_home_button.dart';

class AuthContainer extends StatelessWidget {
  final String subtitle;
  final Widget child;
  final bool showBackButton;

  const AuthContainer({
    super.key,
    required this.subtitle,
    required this.child,
    required this.showBackButton,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      body: Center(
        child: Container(
          constraints: const BoxConstraints(maxWidth: 450),
          padding: const EdgeInsets.all(32),
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(16),
            boxShadow: const [
              BoxShadow(
                color: Colors.black26,
                blurRadius: 20,
                offset: Offset(0, 10),
              ),
            ],
          ),
          child: Form(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                showBackButton ? BackToHomeButton() : SizedBox(),
                const SizedBox(height: 10),
                Icon(Icons.local_shipping, size: 64, color: Colors.blue),
                const SizedBox(height: 10),
                Text(
                  "Logistica App",
                  style: const TextStyle(
                    fontSize: 26,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87,
                  ),
                ),
                const SizedBox(height: 10),
                Text(
                  subtitle,
                  style: const TextStyle(fontSize: 16, color: Colors.black54),
                  textAlign: TextAlign.center,
                ),
                const SizedBox(height: 24),
                child,
              ],
            ),
          ),
        ),
      ),
    );
  }
}
