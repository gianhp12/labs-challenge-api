import 'dart:async';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/auth_module.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:labs_challenge_front/src/shared_module.dart';

class AppModule extends Module {
  @override
  void binds(Injector i) {
   
  }

  @override
  void routes(RouteManager r) {
    r.module('/', module: AuthModule());
  }

  @override
  List<Module> get imports => [
        SharedModule(),
      ];
}

class AuthGuard extends RouteGuard {
  AuthGuard() : super(redirectTo: '/');

  @override
  FutureOr<bool> canActivate(String path, ParallelRoute route) {
    final session = Modular.routerDelegate.navigatorKey.currentContext!
        .read<SessionNotifier>();
    if (session.state.sessionExpired) {
      session.logOut();
      return false;
    }
    session.saveAccessedView(path);
    return true;
  }
}