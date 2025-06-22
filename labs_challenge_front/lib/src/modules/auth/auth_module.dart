import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/repositories/auth_repository.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/pages/auth_page.dart';
import 'package:labs_challenge_front/src/shared_module.dart';

class AuthModule extends Module {
  @override
  void binds(Injector i) {
    //ACTIONS
     i.add<AuthActions>(AuthActions.new);
     //REPOSITORIES
     i.add<AuthRepository>(
        AuthRepositoryImpl.new);
  }

  @override
  void routes(RouteManager r) {
    r.child(
      '/',
      child: (context) => const AuthPage(),
      transition: TransitionType.downToUp,
    );
  }

  @override
  List<Module> get imports => [SharedModule()];
}
