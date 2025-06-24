import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:labs_challenge_front/src/app_module.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_login_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_login_state.dart';
import 'package:labs_challenge_front/src/shared/errors/common_errors.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/services/local_storage/shared_preferences_adapter.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';
import 'package:mocktail/mocktail.dart';
import 'package:result_dart/result_dart.dart';

class MockAuthRepository extends Mock implements AuthRepositoryImpl {}

class MockLocalStorage extends Mock implements LocalStorage {}

void main() {
  late MockAuthRepository mockRepository;
  late MockLocalStorage mockLocalStorage;
  late AuthLoginActions actions;
  late LoggedUser loggedUser;
  late SessionNotifier session;

  setUp(() async {
    mockRepository = MockAuthRepository();
    mockLocalStorage = MockLocalStorage();
    session = SessionNotifier(localStorage: mockLocalStorage);
    Modular.bindModule(AppModule(sessionNotifier: session));
    actions = AuthLoginActions(mockRepository);
    loggedUser = LoggedUser(
      token: "token",
      email: "test@example.com",
      name: "Test User",
      expiresIn: 123456,
      isEmailConfirmed: true,
    );
  });
  group("login", () {
    test("Should return getted state when repository return success", () async {
      //GIVEN
      when(
        () => mockRepository.login(any(), any()),
      ).thenAnswer((_) async => Success(loggedUser));
      when(
        () => mockLocalStorage.saveString(any(), any()),
      ).thenAnswer((_) async {});
      //WHEN
      await actions.login("test@example.com", "password");
      //THEN
      expect(actions.currentState, isA<GettedAuthLoginState>());
    });

    test("Should return error state when repository returns failure", () async {
      //GIVEN
      when(() => mockRepository.login(any(), any())).thenAnswer(
        (_) async => Failure(
          ConnectionError(
            errorMessage: "Invalid credentials",
            errorModule: 'auth',
            errorMethod: 'login',
          ),
        ),
      );
      //WHEN
      await actions.login("test@example.com", "wrongpassword");
      //THEN
      expect(actions.currentState, isA<ErrorAuthLoginState>());
    });
  });
}
