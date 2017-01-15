call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86
msbuild	/t:Build;PipelinePreDeployCopyAllFilesToOneFolder		/p:Configuration=Debug;_PackageTempDir=..\deploy\Elmah.Logger.Web.Debug	Elmah.Logger.Web.csproj

