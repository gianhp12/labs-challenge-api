import 'dart:async';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/auth_module.dart';
import 'package:labs_challenge_front/src/modules/home/home_module.dart';
import 'package:labs_challenge_front/src/shared/pages/app_splash_page.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:labs_challenge_front/src/shared_module.dart';

class AppModule extends Module {
  final SessionNotifier sessionNotifier;

  AppModule({required this.sessionNotifier});

  @override
  void binds(Injector i) {
    i.addInstance<SessionNotifier>(sessionNotifier);
  }

  @override
  void routes(RouteManager r) {
    r.child('/', child: (_) => const AppSplashPage());
    r.module('/auth', module: AuthModule());
    r.module('/home', module: HomeModule(), guards: [AuthGuard()]);
  }

  @override
  List<Module> get imports => [SharedModule()];
}

class AuthGuard extends RouteGuard {
  AuthGuard() : super(redirectTo: '/');

  @override
  FutureOr<bool> canActivate(String path, ParallelRoute route) {
    try {
      final session = Modular.get<SessionNotifier>();
      if (session.state.sessionExpired) {
        session.logOut("Sua sessão expirou realize um novo login", false);
        return false;
      }
      if (session.state.loggedUser == null) {
        session.logOut("Necessário fazer o login para continuar" , false);
        return false;
      }
      session.saveAccessedView(path);
      return true;
    } catch (e) {
      return false;
    }
  }
}
