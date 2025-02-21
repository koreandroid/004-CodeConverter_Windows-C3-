# 004-CodeConverter_Windows-C3-
## 프로젝트 내용
### 정의
- 간단한 파이썬 코드를 같은 동작을 수행하는 C\#으로 작성된 코드로 변환해주는 애플리케이션

### 기술 스택
- C\#
- C\+\+ **\(예정\)**
- Python
- \.NET Framework \(Windows Forms\)

### 향후 개선 방향
- **옵저버 패턴을 학습하여 양방향 바인딩을 구현하기\.** 현재는 뷰\(_Form1\.cs_\)에서 변경 사항이 발생하면 뷰모델\(_Form1ViewModel\.cs_\)의 프로퍼티가 동기화 되는 로직으로 구현되어 있다.
- **구문 분석 오류를 커스텀 Exception 클래스로 구현함으로써 오류 상황을 구체적으로 식별하고 적절한 로그를 작성하기\.**

## 프로젝트 구성
### /Form1\.cs
- 애플리케이션 실행시 열리는 초기 화면으로서 원본 코드를 작성할 수 있고, 로깅 히스토리와 변환하기 버튼을 포함하고 있습니다\.

### /Form2\.cs
- 결과 화면으로서 초기 화면에서 작성한 원본 코드와 애플리케이션에서 변환한 결과 코드를 동시에 보여줍니다\.

### /ErrorDialogForm\.cs
- 1단계 유효성 검사 오류 및 2단계 구문 분석 오류를 dialog로 나타내는 뷰에 대한 파일입니다\.

### /Models/Converter/CodeConverter\.cs
- 코드 변환 기능이 구현된 모델 클래스입니다\. 추상 클래스로써 Python 코드를 다른 언어로 변환하기 위한 로직들이 구현되어 있습니다\.

### /Models/Converter/ToC3CodeConverter\.cs
- CodeConverter 클래스를 확장하여 Python 원본 코드가 구문 분석 과정을 거쳐 Result 프로퍼티에 C\# 결과 코드가 저장될 수 있도록 구현되어 있습니다\.

### /Models/Converter/ToCppCodeConverter\.cs \(예정\)
- CodeConverter 클래스를 확장하여 Python 원본 코드가 구문 분석 과정을 거쳐 Result 프로퍼티에 C\+\+ 결과 코드가 저장될 수 있도록 구현 예정입니다\.

### /ViewModels/Form1ViewModel\.cs
- Form1 뷰에 대한 뷰모델을 구현한 클래스로서 Form1ViewModel\.ValidateSourceCode\(\), Form1ViewModel\.ParseWith\(\), 그리고 Form1ViewModel\.GetFullErrorMessage\(\)와 같은 메소드들을 제공합니다\.