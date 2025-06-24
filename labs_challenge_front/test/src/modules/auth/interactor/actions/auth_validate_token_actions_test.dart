import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:labs_challenge_front/src/app_module.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_validate_token_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_validate_token_state.dart';
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
  late AuthValidateTokenActions actions;
  late LoggedUser loggedUser;
  late SessionNotifier session;

  setUp(() {
    mockRepository = MockAuthRepository();
    mockLocalStorage = MockLocalStorage();
    session = SessionNotifier(localStorage: mockLocalStorage);
    Modular.bindModule(AppModule(sessionNotifier: session));
    actions = AuthValidateTokenActions(mockRepository);
    loggedUser = LoggedUser(
      token: "token",
      email: "test@example.com",
      name: "Test User",
      expiresIn: 123456,
      isEmailConfirmed: true,
    );
  });

  group("validateToken", () {
    test(
      "Should return success state when repository return success",
      () async {
        //GIVEN
        final email = "user@gmail.com";
        final password = "password-test";
        final token = "token-test";
        when(
          () => mockRepository.validateToken(any(), any()),
        ).thenAnswer((_) async => Success(1));
        when(
          () => mockRepository.login(any(), any()),
        ).thenAnswer((_) async => Success(loggedUser));
        when(
          () => mockLocalStorage.saveString(any(), any()),
        ).thenAnswer((_) async {});
        //WHEN
        await actions.validateToken(email, password, token);
        //THEN
        expect(actions.currentState, isA<SuccessAuthValidateTokenState>());
      },
    );

    test("Should return error when repository return error", () async {
      //GIVEN
      final email = "user@gmail.com";
      final password = "password-test";
      final token = "token-test";
      when(() => mockRepository.validateToken(any(), any())).thenAnswer(
        (_) async => Failure(
          ConnectionError(
            errorMessage: "Invalid token",
            errorModule: 'auth',
            errorMethod: 'login',
          ),
        ),
      );
      //WHEN
      await actions.validateToken(email, password, token);
      //THEN
      expect(actions.currentState, isA<ErrorAuthValidateTokenState>());
    });
  });

  group("resendToken", () {
    test(
      "Should return success state when repository return success",
      () async {
        //GIVEN
        final email = "user@gmail.com";
        when(
          () => mockRepository.resendToken(any()),
        ).thenAnswer((_) async => Success(1));
        //WHEN
        await actions.resendToken(email);
        //THEN
        expect(
          actions.currentState,
          isA<SuccessAuthValidateResendTokenState>(),
        );
      },
    );

    test("Should return error when repository return error", () async {
      //GIVEN
      final email = "user@gmail.com";
      when(() => mockRepository.resendToken(any())).thenAnswer(
        (_) async => Failure(
          ConnectionError(
            errorMessage: "Wait 5 minutes for a new token",
            errorModule: 'auth',
            errorMethod: 'login',
          ),
        ),
      );
      //WHEN
      await actions.resendToken(email);
      //THEN
      expect(actions.currentState, isA<ErrorAuthValidateTokenState>());
    });
  });
}
