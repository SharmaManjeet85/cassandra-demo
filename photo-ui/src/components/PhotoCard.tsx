interface Props {
  photo: {
    id: string;
    title: string;
    url: string;
  };
}

export default function PhotoCard({
  photo,
}: Props) {
  return (
    <div className="
      bg-white
      rounded-xl
      shadow
      overflow-hidden
    ">
      <img
        src={photo.url}
        alt={photo.title}
        className="
          w-full
          h-56
          object-cover
        "
      />

      <div className="p-4">
        <h2 className="font-semibold">
          {photo.title}
        </h2>
      </div>
    </div>
  );
}