import 'package:mock_tests/core/service_models/identity/login_response_service_model.dart';
import 'package:mock_tests/core/service_models/identity/login_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_error_details_service_model.dart';
import 'package:mock_tests/core/service_models/identity/register_user_service_model.dart';
import 'package:mock_tests/core/services/identity/identity_service_abstract.dart';

class IdentityController {
  static final IdentityController _instance = IdentityController._();

  static late final IdentityServiceAbstract _identityService;

  IdentityController._();
  factory IdentityController(IdentityServiceAbstract identityService) {
    _identityService = identityService;
    return _instance;
  }

  Future<RegisterErrorDetailsServiceModel> register(RegisterServiceModel serviceModel) async {
    return await _identityService.register(serviceModel);
  }

  Future<LoginResponseServiceModel> login(LoginServiceModel serviceModel) async {
    return await _identityService.login(serviceModel);
  }
}
