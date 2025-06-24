import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/home/interactor/states/home_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';

class HomeActions extends StateNotifier<HomeState> {
  HomeActions() : super(HomeState.start());

  void logout(String? message, bool isSuccess) async {
    final session = Modular.get<SessionNotifier>();
    session.logOut(message, isSuccess);
    Modular.to.navigate('/');
  }

  void getInfoUser() async {
    notifySetState((state) => state.setLoading());
    final session = Modular.get<SessionNotifier>();
    var user = session.state.loggedUser;
    notifySetState((state) => state.setUser(user!));
  }

  void setSessionMessage(String message) async {
    final session = Modular.get<SessionNotifier>();
    session.setSessionMessage(message);
  }
}
