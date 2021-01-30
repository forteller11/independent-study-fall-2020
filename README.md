### *the webcam's g👁ze*
###### an independent study at Concordia University, Fall 2020
a graphics framework, and game, created  in C# to learn OpenGL (OpenTK).

---
#### a features and wishlist

###### entity architecture
- [x] object-oriented (cache unfriendly) entity system
- [ ] support for component-based entity system
- [ ] support for data-oriented entity system which stores all like components in array of structs and have systems which act on them. 

###### common gameplay engine features
- [x] support for transform hierachies (parent-child relationships), currently unoptimized (not cached)
- [x] walking sim type 2D movement system where walkable topology can be generated from meshes
- [x] raycasting against individual triangle, plane and sphere colliders, an entity's colliders, or the world's colliders
- [ ] physics integration (likely gpu-accelerated)

###### rendering
- [x] forward rendering
- [x] scriptable offscreen buffers
- [x] multiple lights (direction and point)
- [x] normal mapping, with roughness maps
- [ ] reflections/light probes
- [x] extendable material system that supports sending custom attributes per material or per entity
- [x] shadow mapping (doesn't follow camera)
- [ ] texture atlas support
- [ ] texture streaming and dynamic atlas creation 
- [ ] static batching (of static geometry at runtime)
- [ ] dynamic batching

###### misc
- [ ] serialization
- [ ] editor gui
- [ ] audio
- [ ] scripts that can be compiled seperately from engine (multiple assemblies)
- [ ] simple multi-threading, so that render loop, entity stuff, and audio stuff happen on seperate threads.
- [ ] profiling 
