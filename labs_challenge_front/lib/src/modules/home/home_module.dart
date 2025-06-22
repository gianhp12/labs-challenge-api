import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/home/pages/home_page.dart';
import 'package:labs_challenge_front/src/shared_module.dart';

class HomeModule extends Module {
  @override
  void routes(RouteManager r) {
    r.child(
      '/',
      child: (context) => const HomePage(),
      transition: TransitionType.fadeIn,
    );
  }

  @override
  List<Module> get imports => [SharedModule()];
}
