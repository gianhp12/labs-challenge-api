import 'package:flutter_test/flutter_test.dart';
import 'package:labs_challenge_front/flavor_config.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/shared/errors/common_errors.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_error.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_respose_model.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_service.dart';
import 'package:mocktail/mocktail.dart';

class MockHttpService extends Mock implements HttpServiceImpl {}

void main() {
  late AuthRepositoryImpl mockRepository;
  late HttpServiceImpl mockHttpService;

  setUp(() async {
    flavor = Flavor.dev;
    mockHttpService = MockHttpService();
    mockRepository = AuthRepositoryImpl(mockHttpService);
  });

  group("login", () {
    test("Should return Success when http return ok", () async {
      //GIVEN
      final mockResponseJson = '''{
        "username": "Test User",
        "email": "test@example.com",
        "isEmailConfirmed": true,
        "accessToken": "token-abc",
        "expiresIn": 300
      }''';
      when(
        () => mockHttpService.post(any(), body: any(named: 'body')),
      ).thenAnswer(
        (_) async => HttpResponse(
          requestBody: "",
          statusMsg: "OK",
          data: mockResponseJson,
          statusCode: 200,
        ),
      );
      final email = 'test@example.com';
      final password = 'password123';
      //WHEN
      final result = await mockRepository.login(email, password);
      //THEN
      result.fold(
        (success) {
          expect(success, isA<LoggedUser>());
        },
        (failure) {
          fail("Should return a logged user, test fail");
        },
      );
    });

    test(
      "Should return a Connection Error when http return BadRequest",
      () async {
        //GIVEN
        final errorJson = '''{
        "Message": "message-test",
        "StatusCode": 500,
        "TraceId": "traceid-test"
      }''';
        when(
          () => mockHttpService.post(any(), body: any(named: 'body')),
        ).thenThrow(BadRequest(error: errorJson));
        final email = "teste@gmail.com";
        final password = "test-password";
        //WHEN
        final result = await mockRepository.login(email, password);
        //THEN
        result.fold(
          (success) {
            fail("Test fail, should return a ConnectionError");
          },
          (failure) {
            expect(failure, isA<ConnectionError>());
          },
        );
      },
    );
  });

  group("register", () {
    test("Should return Success when http return no content", () async {
      //GIVEN
      when(
        () => mockHttpService.post(any(), body: any(named: 'body')),
      ).thenAnswer(
        (_) async => HttpResponse(
          requestBody: "",
          statusMsg: "OK",
          data: null,
          statusCode: 204,
        ),
      );
      final username = "user";
      final email = "teste@gmail.com";
      final password = "12345";
      //WHEN
      final result = await mockRepository.register(username, email, password);
      //THEN
      expect(result.isSuccess(), true);
    });

    test(
      "Should return a Connection Error when http return BadRequest",
      () async {
        //GIVEN
        final errorJson = '''{
        "Message": "message-test",
        "StatusCode": 500,
        "TraceId": "traceid-test"
      }''';
        when(
          () => mockHttpService.post(any(), body: any(named: 'body')),
        ).thenThrow(BadRequest(error: errorJson));
        final email = "teste@gmail.com";
        final password = "test-password";
        //WHEN
        final result = await mockRepository.login(email, password);
        //THEN
        result.fold(
          (success) {
            fail("Test fail, should return a ConnectionError");
          },
          (failure) {
            expect(failure, isA<ConnectionError>());
          },
        );
      },
    );
  });

  group("validateToken", () {
    test("Should return Success when http return no content", () async {
      //GIVEN
      when(
        () => mockHttpService.post(any(), body: any(named: 'body')),
      ).thenAnswer(
        (_) async => HttpResponse(
          requestBody: "",
          statusMsg: "OK",
          data: null,
          statusCode: 200,
        ),
      );
      final email = "teste@gmail.com";
      final token = "token-test";
      //WHEN
      final result = await mockRepository.validateToken(email, token);
      //THEN
      expect(result.isSuccess(), true);
    });

    test(
      "Should return a Connection Error when http return BadRequest",
      () async {
        //GIVEN
        final errorJson = '''{
        "Message": "message-test",
        "StatusCode": 500,
        "TraceId": "traceid-test"
      }''';
        when(
          () => mockHttpService.post(any(), body: any(named: 'body')),
        ).thenThrow(BadRequest(error: errorJson));
        final email = "teste@gmail.com";
        final token = "test-password";
        //WHEN
        final result = await mockRepository.validateToken(email, token);
        //THEN
        result.fold(
          (success) {
            fail("Test fail, should return a ConnectionError");
          },
          (failure) {
            expect(failure, isA<ConnectionError>());
          },
        );
      },
    );
  });

  group("resendToken", () {
    test("Should return Success when http return no content", () async {
      //GIVEN
      when(
        () => mockHttpService.post(any(), body: any(named: 'body')),
      ).thenAnswer(
        (_) async => HttpResponse(
          requestBody: "",
          statusMsg: "OK",
          data: null,
          statusCode: 202,
        ),
      );
      final email = "teste@gmail.com";
      //WHEN
      final result = await mockRepository.resendToken(email);
      //THEN
      expect(result.isSuccess(), true);
    });

    test(
      "Should return a Connection Error when http return BadRequest",
      () async {
        //GIVEN
        final errorJson = '''{
        "Message": "message-test",
        "StatusCode": 500,
        "TraceId": "traceid-test"
      }''';
        when(
          () => mockHttpService.post(any(), body: any(named: 'body')),
        ).thenThrow(BadRequest(error: errorJson));
        final email = "teste@gmail.com";
        //WHEN
        final result = await mockRepository.resendToken(email);
        //THEN
        result.fold(
          (success) {
            fail("Test fail, should return a ConnectionError");
          },
          (failure) {
            expect(failure, isA<ConnectionError>());
          },
        );
      },
    );
  });
}
