import 'package:flutter/cupertino.dart';
import 'package:labs_challenge_front/src/shared/hooks/use_state.dart';

class AuthPage extends StatefulWidget {
  const AuthPage({super.key});

  @override
  State<AuthPage> createState() => _AuthPageState();
}

class _AuthPageState extends State<AuthPage> with UseState {
  @override
  Widget build(BuildContext context) {
    return Center(child: Text("Tela de Login"));
  }
}
