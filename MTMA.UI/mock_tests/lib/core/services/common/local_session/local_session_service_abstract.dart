import 'dart:async';

import 'package:flutter/material.dart';
import 'package:local_session_timeout/local_session_timeout.dart';

abstract class LocalSessionServiceAbstract {
  StreamController<SessionState> get sessionStateStream;
  SessionConfig get sessionConfig;

  void startListening({required GlobalKey<NavigatorState> navigatorKey});
}
