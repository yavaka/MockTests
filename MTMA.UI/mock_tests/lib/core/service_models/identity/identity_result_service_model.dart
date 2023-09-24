import 'dart:convert';

class IdentityResultServiceModel {
  bool succeeded;
  List<String> errors;

  IdentityResultServiceModel({
    required this.succeeded,
    this.errors = const [],
  });

  factory IdentityResultServiceModel.fromJson(dynamic json) {
    json = jsonDecode(json);
    return IdentityResultServiceModel(
      succeeded: json['succeeded'],
      errors: List<String>.from(json['errors']),
    );
  }
}
