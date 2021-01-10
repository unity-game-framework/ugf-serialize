# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.1](https://github.com/unity-game-framework/ugf-serialize/releases/tag/3.0.1) - 2021-01-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/10?closed=1)  
    

### Fixed

- Fix SerializerJsonNetConvertTypesAssetEditor does not have compile define ([#42](https://github.com/unity-game-framework/ugf-serialize/pull/42))  
    - Fix missing `UGF_SERIALIZE_JSONNET` compile define.

## [3.0.0](https://github.com/unity-game-framework/ugf-serialize/releases/tag/3.0.0) - 2021-01-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/9?closed=1)  
    

### Added

- Add serializer builder ([#39](https://github.com/unity-game-framework/ugf-serialize/pull/39))  
    - Add `SerializerBuilderBase` and `SerializerBuilder<TData>`.
    - Add implementation of `IBuilder<ISerializer<TData>>` for `SerializerAsset<TData>` class.

### Removed

- Remove name and data type from serializer builder ([#38](https://github.com/unity-game-framework/ugf-serialize/pull/38))  
    - Remove `Name` and `DataType` properties from `ISerializerBuilder`.
    - Change `ISerializerProvider` to work with ids instead of names.

## [2.1.0](https://github.com/unity-game-framework/ugf-serialize/releases/tag/2.1.0) - 2020-12-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/8?closed=1)  
    

### Added

- Add option to extend default serializers ([#32](https://github.com/unity-game-framework/ugf-serialize/pull/32))  
    - Add `SerializerJsonNet.OnSerialize()` and `OnDeserialize()` overridable methods used to implement custom `JsonNet` serialization.
    - Add `SerializerJsonNetCustom` class to implement `JsonNet` serialization with custom reader and writer.
    - Add `SerializerJsonNetConvertNames` and `SerializerJsonNetConvertNamesAsset` to create and use `JsonNet` serializer which can convert property names during serialization and deserialization.
    - Add `SerializerJsonNetConvertTypesAsset` which create `SerializerJsonNetConvertNames` serializer with `ConvertTypeNameBinder` binder used to convert type information for specific types.
    - Add `SerializerYaml.OnSerialize()` and `OnDeserialize()` overridable methods used to implement custom serialization with `YamlDotNet`.
    - Change `SerializerJsonNet` to use `JsonSerializerSettings` settings for `JsonNet` serialization and `Indent` value to specify indentation with readable formatting.
    - Change `SerializerYaml` to use specified `ISerializer` and `IDeserializer` serializers from `YamlDotNet`.

## [2.0.0](https://github.com/unity-game-framework/ugf-serialize/releases/tag/2.0.0) - 2020-12-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/7?closed=1)  
    

### Changed

- Update package with builders package ([#29](https://github.com/unity-game-framework/ugf-serialize/pull/29))  
    - Add dependency package: `com.ugf.builder` of `2.0.0` version.
    - Change `com.ugf.editortools` to `1.7.0` version.
    - Change create menu root names from `UGF` to `Unity Game Framework`.
    - Change `ISerializerBuilder` interface to inherit from `IBuilder<ISerializer>`.
    - Change `SerializerAsset` class to inherit from `BuilderAsset<ISerializer>`.

## [1.3.0](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.3.0) - 2020-11-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/6?closed=1)  
    

### Added

- Add object copy using specific serializer ([#26](https://github.com/unity-game-framework/ugf-serialize/pull/26))  
    - Add `SerializeUtility` with `Copy` and `Copy<T>` methods to copy specified target using specific serializer.
    - Add default constructors for `SerializerUnityJsonBytes` and `SerializerFormatter` serializers.
    - Add `SerializerFormatterBinaryAsset.OnCreateFormatter` overridable method to control formatter creation.
    - Add `SerializerUnityJsonBytesAsset.OnCreateEncoding` overridable method to control encoding creation.

## [1.2.0](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.2.0) - 2020-10-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/5?closed=1)  
    

### Added

- Add optional serializers when UGF.JsonNet and UGF.Yaml packages available ([#24](https://github.com/unity-game-framework/ugf-serialize/pull/24))  
    - Add `SerializerJsonNet` serializer and `SerializerJsonNetAsset` asset to use serialization when `UGF.JsonNet` package available in project.
    - Add `SerializerYaml` serializer and `SerializerYamlAsset` asset to use serialization when `UGF.Yaml` package available in project.
- Add Unity Editor Yaml serializer ([#21](https://github.com/unity-game-framework/ugf-serialize/pull/21))  
    - Add `SerializerUnityYamlEditor` serializer to use Unity Editor Yaml serialization.
    - Add `SerializerUnityYamlEditorAsset` scriptableobject asset to handler creation of `SerializerUnityYamlEditor` serializer.
    - Add `com.ugf.editortools` of `1.3.1` version dependency.
- Add Unity Editor Json serializer ([#20](https://github.com/unity-game-framework/ugf-serialize/pull/20))  
    - Add `SerializerUnityJsonEditor` to serialize target using Unity Editor Json serialization.
    - Add `SerializerUnityJsonEditorAsset` to handler creation of `SerializerUnityJsonEditor` serializer.
- Add Serializer handler asset ([#19](https://github.com/unity-game-framework/ugf-serialize/pull/19))  
    - Add `ISerializerBuilder` interface to implement custom serializer builder.
    - Add `SerializerAsset` and `SerializerAsset<T>` scriptableobject classes to define custom serializer builder as asset.
    - Add `SerializerFormatterBinaryAsset` scriptableobject asset to handle creation of `SerializerFormatter` with `BinaryFormatter`.
    - Add `SerializerUnityJsonAsset` and `SerializerUnityJsonBytesAsset` scritpableobject assets to handler creation of `SerializerUnityJson` and `SerializerUnityJsonBytes` with specific options.
    - Remove `SerializerUnityJsonUtility` and `SerializerFormatterUtility` classes.
- Add async method for Serializers ([#18](https://github.com/unity-game-framework/ugf-serialize/pull/18))  
    - Add `SerializerAsyncBase<T>` abstract class to implement serializer with async methods.
    - Add `ISerializerAsync` and `ISerializerAsync<T>` interfaces to implement custom async serializers.
    - Add profiling for `SerializerFormatter`, `SerializerUnityJson` and `SerializerUnityJsonBytes` to display information in profiler.
    - Change `SerializerFormatter`, `SerializerUnityJson` and `SerializerUnityJsonBytes` to support async methods.

### Changed

- Update to Unity 2020.2 ([#14](https://github.com/unity-game-framework/ugf-serialize/issues/14))

## [1.1.0-preview](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.1.0-preview) - 2019-11-23  

- [Commits](https://github.com/unity-game-framework/ugf-serialize/compare/1.0.0-preview.2...1.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/4?closed=1)

### Changed
- Update to Unity 2019.3.
- `SerializerProvider`: small refactoring.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.0.0-preview.2) - 2019-08-16  

- [Commits](https://github.com/unity-game-framework/ugf-serialize/compare/1.0.0-preview.1...1.0.0-preview.2)
- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/3?closed=1)

### Fixed
- `SerializerBase`: made `Deserialize<T>(TData data)` as virtual.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.0.0-preview.1) - 2019-08-04  

- [Commits](https://github.com/unity-game-framework/ugf-serialize/compare/1.0.0-preview...1.0.0-preview.1)
- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/2?closed=1)

### Added
- Package short description.

### Changed
- Update to Unity 2019.2.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-serialize/releases/tag/1.0.0-preview) - 2019-07-21  

- [Commits](https://github.com/unity-game-framework/ugf-serialize/compare/1ad1064...1.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-serialize/milestone/1?closed=1)

### Added
- This is a initial release.


