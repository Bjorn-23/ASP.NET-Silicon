using Business.Models;
using Infrastructure.Entitites;
using System.Diagnostics;

namespace Business.Factories;

public class SavedCoursesFactory
{
    public static SavedCoursesModel Create(SavedCoursesEntity entity)
    {
        try
        {
            return new SavedCoursesModel
            {
                CourseId = entity.CourseId,
                UserId = entity.UserId,
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static SavedCoursesEntity Create(SavedCoursesModel model)
    {
        try
        {
            return new SavedCoursesEntity
            {
                CourseId = model.CourseId,
                UserId = model.UserId,
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static IEnumerable<SavedCoursesModel> Create(IEnumerable<SavedCoursesEntity> entities)
    {
        try
        {
            var models = new List<SavedCoursesModel>();
            foreach (var entity in entities)
            {
                models.Add(Create(entity));
            }
            
            return models;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static IEnumerable<SavedCoursesEntity> Create(IEnumerable<SavedCoursesModel> models)
    {
        try
        {
            var entities = new List<SavedCoursesEntity>();
            foreach (var model in models)
            {
                entities.Add(Create(model));
            }

            return entities;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
