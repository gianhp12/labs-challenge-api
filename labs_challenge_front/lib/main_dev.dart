import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/flavor_config.dart';
import 'package:labs_challenge_front/src/app_module.dart';
import 'package:labs_challenge_front/src/app_widget.dart';

void main() async {
  flavor = Flavor.dev;
  runApp(ModularApp(module: AppModule(), child: AppWidget()));
}
