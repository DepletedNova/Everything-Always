using Kitchen.Layouts;
using Kitchen.Layouts.Features;
using Kitchen.Layouts.Modules;

namespace EverythingAlways.Modules
{
    public class FilterBySide : LayoutModule
    {
        public bool Horizontal;

        public override void ActOn(LayoutBlueprint blueprint)
        {
            blueprint.Features.RemoveAll((Feature f) => !Horizontal ? f.Tile1.x != f.Tile2.x : f.Tile1.y != f.Tile2.y);
        }
    }
}
