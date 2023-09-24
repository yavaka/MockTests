import 'package:get_it/get_it.dart';
import 'package:mock_tests/core/services/identity/identity_service.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';
import 'package:mock_tests/ui/controllers/identity_controller.dart';

GetIt locator = GetIt.instance;

// IMPORTANT: Always register services before controllers
void setupLocator() {
  registerServices();
  registerControllers();
}

void registerServices() {
  locator.registerFactory<IdentityServiceAbstract>(() => IdentityService());
}

void registerControllers() {
  locator.registerLazySingleton(
    () => IdentityController(locator<IdentityServiceAbstract>()),
  );
}
