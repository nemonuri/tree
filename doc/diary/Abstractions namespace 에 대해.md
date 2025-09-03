# Abstractions package 에 대해

- Abstractions package 에는, 핵심 이론에 대한 인터페이스가 정의되어 있어야 한다.
  - Abstractions package 는 'Dependency Injection'을 위한 것이기 때문이다.
  - 즉, '~Theory' 클래스는 사실 Abstractions package 에 인터페이스로 정의되어 있어야 하고, 기본 구현은 일반 package 에 정의되어 있어야 한다.
  - 그런데 뭐...일단 기본 Theory 를 잘 구현하고, DI 확장은 나중에 생각해도 되는 거 아닐까?