**Project Description**
The serialization framework that cares for bits and speed :)

The goal of the project is to have very fast serializer and deserializer, that produces significantly smaller serialization results then any other serializer available on the market.

The goals of the project are:
* should do serialization and deserialization much faster then the default .Net binary or text based serialization (like xml),
* should produce much smaller serialization results,
* should generate all the source code necessary for contract based serialization (for all the platforms it supports),
* should be easy to port to C++ for usage with WinRT (as part of ACF - Asynchronous Communication Framework. It's message driven framework),
* should be easy to port to Java for usage with Android/Linux based tablets.

This project is part of Azure Entity Framework I'm working on now and will be used as main serialization engine for asynchronous, message driven communication between .Net (on server side) and WinRT/Android based tablets (as part of ACF).

The current tests have shown it's about **8 times faster** then binary serialization available as part of .Net Framework and produces about **10-12 times smaller** result sets.

This is initial version of the project and it contains all you need to generate your own serializers. The nearest one version I plan to publish should support generics also (especially Nullable<> should be valuable)