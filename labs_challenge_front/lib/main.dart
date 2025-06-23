import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/flavor_config.dart';
import 'package:labs_challenge_front/src/app_module.dart';
import 'package:labs_challenge_front/src/app_widget.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  flavor = Flavor.prod;
  final session = SessionNotifier();
  await session.loadSession();
  runApp(ModularApp(module: AppModule(sessionNotifier: session), child: AppWidget()));
}
