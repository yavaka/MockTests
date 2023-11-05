import 'package:get_it/get_it.dart';
import 'package:mock_tests/core/services/account/account_service_abstract.dart';
import 'package:mock_tests/core/services/common/local_session/local_session_service.dart';
import 'package:mock_tests/core/services/common/local_session/local_session_service_abstract.dart';
import 'package:mock_tests/core/services/identity/identity_service.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';
import 'package:mock_tests/ui/controllers/account_controller.dart';
import 'package:mock_tests/ui/controllers/identity_controller.dart';

GetIt serviceLocator = GetIt.instance;

// IMPORTANT: Always register services before controllers
void setupLocator() {
  registerServices();
  registerControllers();
}

void registerServices() {
  serviceLocator.registerFactory<IdentityServiceAbstract>(() => IdentityService());
  serviceLocator.registerSingleton<LocalSessionServiceAbstract>(LocalSessionService());
}

void registerControllers() {
  serviceLocator.registerLazySingleton(
    () => IdentityController(serviceLocator<IdentityServiceAbstract>()),
  );
  serviceLocator.registerLazySingleton(
    () => AccountController(serviceLocator<AccountServiceAbstract>()),
  );
}
